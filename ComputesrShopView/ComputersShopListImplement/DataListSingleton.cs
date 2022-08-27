﻿using ComputersShopContracts.Enums;
using ComputersShopFileImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ComputersShopListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        private readonly string ComponentFileName = "Component.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string ComputerFileName = "Computer.xml";
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Computer> Computers { get; set; }
        private DataListSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            Computers = LoadComputers();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
        ~DataListSingleton()
        {
            SaveComponents();
            SaveOrders();
            SaveComputers();
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
            // прописать логику
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
            // прописать логику
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
    }
}
