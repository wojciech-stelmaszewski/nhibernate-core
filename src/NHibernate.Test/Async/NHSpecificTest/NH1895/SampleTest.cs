﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH1895
{
	using System.Threading.Tasks;
	[TestFixture]
	public class SampleTestAsync : BugTestCase
	{
		[Test]
		public async Task SaveTestAsync()
		{
			var o = new Order {Id = Guid.NewGuid(), Name = "Test Order"};
			for (int i = 0; i < 5; i++)
			{
				var d = new Detail {Id = Guid.NewGuid(), Name = "Test Detail " + i, Parent = o};
				o.Details.Add(d);
			}
			using (ISession session = OpenSession())
			{
				await (session.SaveAsync(o));
				await (session.FlushAsync());
			}
			using (ISession session = OpenSession())
			{
				await (session.DeleteAsync(o));
				await (session.FlushAsync());
			}
		}
	}
}
