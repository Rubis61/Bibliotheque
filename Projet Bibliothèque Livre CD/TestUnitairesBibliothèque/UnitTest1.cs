using System;
using Projet_Bibliothèque_Livre_CD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestUnitairesBibliothèque
{
    [TestClass]
    public class UnitTest1
    {
        public static Bibliotheque bibliothèque = new Bibliotheque(new List<CD>(), new List<Livre>());

        [TestMethod]
        public void TestMethod1()
        {
            
        }
    }
}
