using System;
using System.Collections.Generic;

namespace _16
{
    public class Field
    {
        public Field(){
            this.MatchingRuleIndexes = new List<int>();
        }
        public int Value {get;set;}
        public List<int> MatchingRuleIndexes{get;set;}
    }
    
}