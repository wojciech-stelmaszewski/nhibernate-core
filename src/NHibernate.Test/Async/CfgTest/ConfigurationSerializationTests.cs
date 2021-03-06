﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using NHibernate.Cfg;
using NHibernate.DomainModel;
using NHibernate.Driver;
using NHibernate.Engine;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace NHibernate.Test.CfgTest
{
	using System.Threading.Tasks;
	[TestFixture]
	public class ConfigurationSerializationTestsAsync
	{

		[Test]
		public async Task Basic_CRUD_should_workAsync()
		{
			TestsContext.AssumeSystemTypeIsSerializable();

			Assembly assembly = Assembly.Load("NHibernate.DomainModel");
			var cfg = new Configuration();
			if (TestConfigurationHelper.hibernateConfigFile != null)
			{
				cfg.Configure(TestConfigurationHelper.hibernateConfigFile);
			}
			cfg.AddResource("NHibernate.DomainModel.ParentChild.hbm.xml", assembly);

			var formatter = new BinaryFormatter();
			var memoryStream = new MemoryStream();
			formatter.Serialize(memoryStream, cfg);
			memoryStream.Position = 0;
			cfg = formatter.Deserialize(memoryStream) as Configuration;
			Assert.That(cfg, Is.Not.Null);

			var export = new SchemaExport(cfg);
			await (export.ExecuteAsync(true, true, false));
			var sf = cfg.BuildSessionFactory();
			object parentId;
			object childId;
			using (var session = sf.OpenSession())
			using (var tran = session.BeginTransaction())
			{
				var parent = new Parent();
				var child = new Child();
				parent.Child = child;
				parent.X = 9;
				parent.Count = 5;
				child.Parent = parent;
				child.Count = 3;
				child.X = 4;
				parentId = await (session.SaveAsync(parent));
				childId = await (session.SaveAsync(child));
				await (tran.CommitAsync());
			}

			using (ISession session = sf.OpenSession())
			{
				var parent = await (session.GetAsync<Parent>(parentId));
				Assert.That(parent.Count, Is.EqualTo(5));
				Assert.That(parent.X, Is.EqualTo(9));
				Assert.That(parent.Child, Is.Not.Null);
				Assert.That(parent.Child.X, Is.EqualTo(4));
				Assert.That(parent.Child.Count, Is.EqualTo(3));
				Assert.That(parent.Child.Parent, Is.EqualTo(parent));
			}

			using (ISession session = sf.OpenSession())
			using (ITransaction tran = session.BeginTransaction())
			{
				var p = await (session.GetAsync<Parent>(parentId));
				var c = await (session.GetAsync<Child>(childId));
				await (session.DeleteAsync(c));
				await (session.DeleteAsync(p));
				await (tran.CommitAsync());
			}

			using (ISession session = sf.OpenSession())
			{
				var p = await (session.GetAsync<Parent>(parentId));
				Assert.That(p, Is.Null);
			}

			TestCase.DropSchema(true, export, (ISessionFactoryImplementor)sf);
		}
	}
}
