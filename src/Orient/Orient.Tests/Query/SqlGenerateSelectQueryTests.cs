﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orient.Client;

namespace Orient.Tests.Query
{
    [TestClass]
    public class SqlGenerateSelectQueryTests
    {
        [TestMethod]
        public void ShouldGenerateSelectAlsoNthAsQuery()
        {
            string generatedQuery = new OSqlSelect()
                .Select("foo").As("Foo")
                .Also("bar").As("Bar")
                .Also("baq").Nth(0).As("Baq")
                .From("TestClass")
                .ToString();

            string query =
                "SELECT foo AS Foo, " +
                "bar AS Bar, " +
                "baq[0] AS Baq " +
                "FROM TestClass";

            Assert.AreEqual(generatedQuery, query);
        }

        [TestMethod]
        public void ShouldGenerateSelectWhereQuery()
        {
            string generatedQuery = new OSqlSelect()
                .Select()
                .From("TestClass")
                .Where("foo").Equals("whoa")
                .Or("foo").NotEquals(123)
                .And("foo").Lesser(1)
                .And("foo").LesserEqual(2)
                .And("foo").Greater(3)
                .And("foo").GreaterEqual(4)
                .And("foo").Like("%whoa%")
                .And("foo").IsNull()
                .And("foo").Contains("johny")
                .And("foo").Contains("name", "johny")
                .ToString();

            string query =
                "SELECT " +
                "FROM TestClass " +
                "WHERE foo = 'whoa' " +
                "OR foo != 123 " +
                "AND foo < 1 " +
                "AND foo <= 2 " +
                "AND foo > 3 " +
                "AND foo >= 4 " +
                "AND foo LIKE '%whoa%' " +
                "AND foo IS NULL " +
                "AND foo CONTAINS 'johny' " +
                "AND foo CONTAINS (name = 'johny')";

            Assert.AreEqual(generatedQuery, query);
        }

        [TestMethod]
        public void ShouldGenerateSelectFromDocumentOridQuery()
        {
            ODocument document = new ODocument();
            document.ORID = new ORID(8, 0);

            string generatedQuery = new OSqlSelect()
                .Select("foo", "bar")
                .From(document)
                .ToString();

            string query =
                "SELECT foo, bar " +
                "FROM #8:0";

            Assert.AreEqual(generatedQuery, query);
        }

        [TestMethod]
        public void ShouldGenerateSelectFromDocumentOClassNameQuery()
        {
            ODocument document = new ODocument();
            document.OClassName = "TestClass";

            string generatedQuery = new OSqlSelect()
                .Select("foo", "bar")
                .From(document)
                .ToString();

            string query =
                "SELECT foo, bar " +
                "FROM TestClass";

            Assert.AreEqual(generatedQuery, query);
        }
    }
}
