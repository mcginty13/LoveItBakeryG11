using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Xml;


namespace BakeryClassLibrary
{
    public abstract class Person
    {
        private string mID;
        private string mName;

        public Person(string pName)
        {
            mID = SqlTools.GenerateID(this);
            mName = pName;
        }
        public Person(string pID, string pName)
        {
            mID = pID;
            mName = pName;
        }
        
        public string GetID() { return mID; }
        public string GetName() { return mName; }
    }
}
