using ComputersShopContracts.Enums;
using ComputersShopFileImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ComputersShopFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string ComponentFileName = "Component.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string ComputerFileName = "Computer.xml";
        private readonly string ClientFileName = "Client.xml";
        private readonly string ImplementerFileName = "Implementer.xml";
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Computer> Computers { get; set; }
        public List<Client> Clients { get; set; }
        public List<Implementer> Implementers { get; set; }
        private FileDataListSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            Computers = LoadComputers();
            Clients = LoadClients();
            Implementers = LoadImplementers();
        }

        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }
        public static void SaveFileDataListSingleton()
        {
            instance.SaveComponents();
            instance.SaveOrders();
            instance.SaveComputers();
            instance.SaveClients();
            instance.SaveImplementers();
        }
        private List<Component> LoadComponents()
        {
            var list = new List<Component>();
            if (File.Exists(ComponentFileName))
            {
                var xDocument = XDocument.Load(ComponentFileName);
                var xElements = xDocument.Root.Elements("Component").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Component
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ComponentName = elem.Element("ComponentName").Value
                    });
                }
            }
            return list;
        }
        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                var xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ComputerId = Convert.ToInt32(elem.Element("ComputerId").Value),
                        ClientId = Convert.ToInt32(elem.Element("ClientId").Value),
                        ImplementerId = Convert.ToInt32(elem.Element("ImplementerId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToInt32(elem.Element("Sum").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), elem.Element("Status").Value),
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement = string.IsNullOrEmpty(elem.Element("DateImplement").Value) ? (DateTime?)null : Convert.ToDateTime(elem.Element("DateImplement").Value),
                    });
                }
            }
            return list;
        }
        private List<Computer> LoadComputers()
        {
            var list = new List<Computer>();
            if (File.Exists(ComputerFileName))
            {
                var xDocument = XDocument.Load(ComputerFileName);
                var xElements = xDocument.Root.Elements("Computer").ToList();
                foreach (var elem in xElements)
                {
                    var computerComp = new Dictionary<int, int>();
                    foreach (var component in
                   elem.Element("ProductComponents").Elements("ComputerComponent").ToList())
                    {
                        computerComp.Add(Convert.ToInt32(component.Element("Key").Value),
                       Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new Computer
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ComputerName = elem.Element("ComputerName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value),
                        ComputerComponents = computerComp
                    });
                }
            }
            return list;
        }
        private List<Client> LoadClients()
        {
            var list = new List<Client>();
            if (File.Exists(ClientFileName))
            {
                XDocument xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Client").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ClientFullName = elem.Element("ClientFullName").Value,
                        Login = elem.Element("Login").Value,
                        Password = elem.Element("Password").Value,
                    });
                }
            }
            return list;
        }
        private List<Implementer> LoadImplementers()
        {
            var list = new List<Implementer>();
            if (File.Exists(ImplementerFileName))
            {
                XDocument xDocument = XDocument.Load(ImplementerFileName);
                var xElements = xDocument.Root.Elements("Implementer").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Implementer
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ImplementerFullName = elem.Element("ImplementerFullName").Value,
                        WorkingTime = Convert.ToInt32(elem.Attribute("WorkingTime").Value),
                        PauseTime = Convert.ToInt32(elem.Attribute("PauseTime").Value),
                    });
                }
            }
            return list;
        }
        private void SaveComponents()
        {
            if (Components != null)
            {
                var xElement = new XElement("Components");
                foreach (var component in Components)
                {
                    xElement.Add(new XElement("Component",
                    new XAttribute("Id", component.Id),
                    new XElement("ComponentName", component.ComponentName)));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(ComponentFileName);
            }
        }
        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                    new XAttribute("Id", order.Id),
                     new XElement("ComputerId", order.ComputerId),
                     new XElement("ClientId", order.ClientId),
                     new XElement("ImplementerId", order.ImplementerId),
                     new XElement("Count", order.Count),
                     new XElement("Sum", order.Sum),
                     new XElement("Status", order.Status),
                     new XElement("DateCreate", order.DateCreate),
                     new XElement("DateImplement", order.DateImplement)));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }
        private void SaveComputers()
        {
            if (Computers != null)
            {
                var xElement = new XElement("Computers");
                foreach (var computer in Computers)
                {
                    var compElement = new XElement("ComputerComponents");
                    foreach (var component in computer.ComputerComponents)
                    {
                        compElement.Add(new XElement("ComputerComponent",
                        new XElement("Key", component.Key),
                        new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Computer",
                     new XAttribute("Id", computer.Id),
                     new XElement("ComputerName", computer.ComputerName),
                     new XElement("Price", computer.Price),
                     compElement));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(ComputerFileName);
            }
        }
        private void SaveImplementers()
        {
            if (Implementers != null)
            {
                var xElement = new XElement("Implementers");
                foreach (var implementer in Implementers)
                {
                    xElement.Add(new XElement("Implementer",
                    new XAttribute("Id", implementer.Id),
                    new XElement("ImplementerFullName", implementer.ImplementerFullName),
                    new XElement("WorkingTime", implementer.WorkingTime),
                    new XElement("PauseTime", implementer.PauseTime)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ImplementerFileName);
            }
        }
        private void SaveClients()
        {
            if (Clients != null)
            {
                var xElement = new XElement("Clients");
                foreach (var client in Clients)
                {
                    xElement.Add(new XElement("Client",
                    new XAttribute("Id", client.Id),
                    new XElement("ClientFullName", client.ClientFullName),
                    new XElement("Login", client.Login),
                    new XElement("Password", client.Password)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
        }
    }
}
