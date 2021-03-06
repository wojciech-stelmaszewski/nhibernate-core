﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.Collections;
using System.Data;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.Dates
{
	using System.Threading.Tasks;
	[TestFixture]
	public class DateTime2FixtureAsync : FixtureBaseAsync
	{
		protected override IList Mappings
		{
			get { return new[] {"NHSpecificTest.Dates.Mappings.DateTime2.hbm.xml"}; }
		}

		protected override DbType? AppliesTo()
		{
			return DbType.DateTime2;
		}

		[Test]
		public async Task SavingAndRetrievingTestAsync()
		{
			DateTime Now = DateTime.Now;

			await (SavingAndRetrievingActionAsync(new AllDates {Sql_datetime2 = Now},
			                          entity => DateTimeAssert.AreEqual(entity.Sql_datetime2, Now)));


			await (SavingAndRetrievingActionAsync(new AllDates { Sql_datetime2 = DateTime.MinValue },
									  entity => DateTimeAssert.AreEqual(entity.Sql_datetime2, DateTime.MinValue)));

			await (SavingAndRetrievingActionAsync(new AllDates { Sql_datetime2 = DateTime.MaxValue },
									  entity => DateTimeAssert.AreEqual(entity.Sql_datetime2, DateTime.MaxValue)));
		}

		[Test]
		public Task SaveMillisecondAsync()
		{
			try
			{
				DateTime datetime2 = DateTime.MinValue.AddMilliseconds(123);

				return SavingAndRetrievingActionAsync(new AllDates { Sql_datetime2 = datetime2 },
																entity => Assert.That(entity.Sql_datetime2, Is.EqualTo(datetime2)));
			}
			catch (Exception ex)
			{
				return Task.FromException<object>(ex);
			}
		}
	}
}