using Microsoft.VisualStudio.TestTools.UnitTesting;
using LabMppCsharp.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabMppCsharp.domain;
using LabMppCsharp.utils;
using LabMppCsharp.utils.exceptions;
using LabMppCsharp.utils.mapper;

namespace LabMppCsharp.repositories.Tests
{
    [TestClass()]
    public class DatabaseRepositoryTests
    {

        [TestMethod()]
        public void GetAllTest()
        {
            var mapper = new TicketMapper();
            var databaseConnectionManager = new DatabaseConnectionManager();
            var repository = new DatabaseRepository<Ticket, int>(databaseConnectionManager, "ticketstest", mapper);
            repository.Clear();
            repository.Add(new Ticket(1, 2.0d));
            repository.Add(new Ticket(2, 3.0d));

            var list = repository.GetAll();
            Assert.IsTrue(list.Count.Equals(2));
            Assert.IsTrue(list.Contains(new Ticket(1, 2.0d)));
            Assert.IsTrue(list.Contains(new Ticket(2, 3.0d)));

        }

        [TestMethod()]
        public void AddTest()
        {
            var mapper = new TicketMapper();
            var databaseConnectionManager = new DatabaseConnectionManager();
            var repository = new DatabaseRepository<Ticket, int>(databaseConnectionManager, "ticketstest", mapper);
            repository.Clear();
            repository.Add(new Ticket
            {
                Id = 3,
                Price = 2.0d
            });
            var list = repository.GetAll();
            Assert.IsTrue(list.Contains(new Ticket
            {
                Id = 3,
                Price = 2.0d
            }));
        }

        [TestMethod()]
        public void GetSizeTest()
        {
            var mapper = new TicketMapper();
            var databaseConnectionManager = new DatabaseConnectionManager();
            var repository = new DatabaseRepository<Ticket, int>(databaseConnectionManager, "ticketstest", mapper);
            repository.Clear();
            Assert.AreEqual(repository.GetSize(), 0);
            repository.Add(new Ticket
            {
                Id = 3,
                Price = 2.0d
            });
            Assert.AreEqual(repository.GetSize(), 1);
        }

        [TestMethod()]
        public void FindByIdTest()
        {
            var mapper = new TicketMapper();
            var databaseConnectionManager = new DatabaseConnectionManager();
            var repository = new DatabaseRepository<Ticket, int>(databaseConnectionManager, "ticketstest", mapper);
            repository.Clear();
            repository.Add(new Ticket
            {
                Id = 3,
                Price = 2.0d
            });
            // try
            // {
            Ticket t = repository.FindById(3);
            t.Equals(new Ticket
            {
                Id = 3,
                Price = 2.0d
            });
            Assert.IsTrue(true);
            //  }
            //catch (RepositoryException e)
            // {
            //     Assert.IsFalse(true);
            // }

            try
            {
                t = repository.FindById(2);
                Assert.IsFalse(true);
            }
            catch (RepositoryException e)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod()]
        public void UpdateTest()
        {
            var mapper = new TicketMapper();
            var databaseConnectionManager = new DatabaseConnectionManager();
            var repository = new DatabaseRepository<Ticket, int>(databaseConnectionManager, "ticketstest", mapper);
            repository.Clear();
            repository.Add(new Ticket
            {
                Id = 3,
                Price = 2.0d
            });
            try
            {
                Ticket t = repository.FindById(3);
                Assert.AreEqual(t, new Ticket
                {
                    Id = 3,
                    Price = 2.0d
                });
            }
            catch (RepositoryException e)
            {
                Assert.IsFalse(true);
            }
            repository.Update(new Ticket
            {
                Id = 3,
                Price = 1.0d
            });

            try
            {
                Ticket t = repository.FindById(3);
                Assert.AreNotEqual(t, new Ticket
                {
                    Id = 3,
                    Price = 2.0d
                });
                Assert.AreEqual(t, new Ticket
                {
                    Id = 3,
                    Price = 1.0d
                });
            }
            catch (RepositoryException e)
            {
                Assert.IsFalse(true);
            }
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var mapper = new TicketMapper();
            var databaseConnectionManager = new DatabaseConnectionManager();
            var repository = new DatabaseRepository<Ticket, int>(databaseConnectionManager, "ticketstest", mapper);
            repository.Clear();
            repository.Add(new Ticket
            {
                Id = 3,
                Price = 2.0d
            });
            repository.Add(new Ticket
            {
                Id = 1,
                Price = 3.0d
            });
            repository.Delete(1);
            var list = repository.GetAll();
            Assert.IsTrue(list.Contains(new Ticket
            {
                Id = 3,
                Price = 2.0d
            }));
            Assert.IsFalse(list.Contains(new Ticket
            {
                Id = 1,
                Price = 3.0d
            }));
        }
    }
}