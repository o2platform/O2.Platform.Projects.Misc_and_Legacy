// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using FluentSharp.CoreLib.Interfaces;

namespace O2.Rules.OunceLabs.RulesUtils
{
    public class IndexedO2Rules
    {
        public static Dictionary<string,List<IO2Rule>> indexAll(List<IO2Rule> o2RulesToIndex)
        {
            var indexedO2Rules = new Dictionary<string, List<IO2Rule>>();
            foreach (var o2Rule in o2RulesToIndex)
            {
                if (false == indexedO2Rules.ContainsKey(o2Rule.Signature))
                    indexedO2Rules.Add(o2Rule.Signature, new List<IO2Rule>());
                indexedO2Rules[o2Rule.Signature].Add(o2Rule);
            }
            return indexedO2Rules;
        }

        public static Dictionary<string,List<IO2Rule>> indexOnSinks(List<IO2Rule> o2RulesToIndex)
        {
            var indexedO2Rules = new Dictionary<string, List<IO2Rule>>();
            foreach (var o2Rule in o2RulesToIndex)
                if (o2Rule.RuleType == O2RuleType.Sink || o2Rule.RuleType == O2RuleType.NotASink)
                {
                    if (false == indexedO2Rules.ContainsKey(o2Rule.Signature))
                        indexedO2Rules.Add(o2Rule.Signature, new List<IO2Rule>());
                    indexedO2Rules[o2Rule.Signature].Add(o2Rule);
                }
            return indexedO2Rules;
        }

        public static Dictionary<string, List<IO2Rule>> indexOnSources(List<IO2Rule> o2RulesToIndex)
        {
            var indexedO2Rules = new Dictionary<string, List<IO2Rule>>();
            foreach (var o2Rule in o2RulesToIndex)
                if (o2Rule.RuleType == O2RuleType.Source || o2Rule.RuleType == O2RuleType.NotASource)
                {
                    if (false == indexedO2Rules.ContainsKey(o2Rule.Signature))
                        indexedO2Rules.Add(o2Rule.Signature, new List<IO2Rule>());
                    indexedO2Rules[o2Rule.Signature].Add(o2Rule);
                }
            return indexedO2Rules;
        }

        internal static List<String> getRulesSignatures(List<IO2Rule> o2Rules)
        {
            var rulesSignatures = new List<String>();
            foreach (var o2Rule in o2Rules)
                rulesSignatures.Add(o2Rule.Signature);               
           //    if (false == rulesSignatures.Contains(o2Rule.Signature))
                    
            return rulesSignatures;
        }
    }
}
