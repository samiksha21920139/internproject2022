using NUnit.Framework;
using CLMSlibrary;
using CLMSlibrary.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace NUnitDemo
{
    [TestFixture]
    public class TestsCMS
    {
        Assembly assembly = null;
        Type classAvailableBook = null;
        Type classUserTable = null;
        AvailableBook oT = null;
        BooksManager oEM = null;

        [SetUp]
        public void Setup()
        {
            assembly = Assembly.Load("CLMSlibrary");
            classAvailableBook = assembly.GetType("CLMSlibrary.BooksManager");
            classUserTable = assembly.GetType("CLMSlibrary.UserManager");
            oT = new AvailableBook();
            oEM = new BooksManager();
        }

        [Test]
        public void TestClassAvailableBook()
        {
            if (classAvailableBook == null)
                Assert.Fail("No class with the name 'BookManager' is implemented as required OR Did you change the class name");
        }

        [Test]
        public void TestClassUserTable()
        {
            if (classUserTable == null)
                Assert.Fail("No class with the name 'UserManager' is implemented as required OR Did you change the class name");
        }

        [Test]
        public void TestGetBookList()
        {
            using (var context = new LibraryContext())
            {
                var mock = new Mock<IEnumerable<AvailableBook>>();
                mock.Setup(x => x.GetEnumerator()).Returns(oEM.GetBookList().GetEnumerator());
                int expected = 0;
                expected = mock.Object.Count<AvailableBook>();
                if (expected == 0)
                {
                    Assert.Fail("No Book available.");
                }
            }            
        }

        [Test]
        public int TestPostTeacher()
        {
            using (var context = new LibraryContext())
            {
                oT.BookId = 24;
                oT.BookTitle = "Entity Framework";
                oT.Category = "Programming";
                oT.Author = "Mc Glenn";
                
                oT.Publisher = "Mc Glenn publications";
                oT.Count = 3;
                oEM.PostTeacher(oT);
            }
            using (var context = new LibraryContext())
            {
                var mock = new Mock<IEnumerable<AvailableBook>>();
                mock.Setup(x => x.GetEnumerator()).Returns(oEM.GetBookList().GetEnumerator());
                int expected = 1;
                bool isExists = false;
                int insertid = 0;
                if (expected > 0)
                {
                    foreach(var i in mock.Object)
                    {
                        if (i.BookId.Equals(24))
                        {
                            isExists = true;
                            insertid = i.BookId;
                            break;
                        }
                        else
                        {
                            isExists = false;
                            continue;
                        }
                    }                    
                }
                if (!isExists)
                    Assert.Fail("No Book added.");
                else
                {
                    oEM.DeleteTeacher(insertid);
                } 
                return insertid;
            }

        }
    }
}
