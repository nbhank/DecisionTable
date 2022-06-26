using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DecisionTables.Domain;
using Microsoft.Practices.Unity;

namespace DecisionTables
{
    internal class ContainerExtension : UnityContainerExtension
    {
        private static readonly Predicate<Type> decisionTablePredicate =
            t => { return t.GetInterfaces().Contains(typeof (IEncapsulatedDecisionTableIoc<Type, ComputeTax>)); };

        private static readonly Predicate<Type> decisionEntryPredicate =
            t => { return t.GetInterfaces().Contains(typeof (IComputeTax)) && !t.IsAbstract; };

        private static readonly Predicate<Type> taxAuthorityPredicate =
            t => { return typeof (TaxAuthority).IsAssignableFrom(t) && !t.IsAbstract; };

        protected override void Initialize()
        {
            var scanner = new SimpleScanner();

            //find DecisionTable related entities
            Type decisionTableType = scanner.FindTypes(decisionTablePredicate).First();
            Type[] computerTypes = scanner.FindTypes(decisionEntryPredicate).ToArray();
            IEnumerable<Type> authorityTypes = scanner.FindTypes(taxAuthorityPredicate);

            //Create the decision table, injecting a key extractor
            object decisionTable = Activator.CreateInstance(decisionTableType,
                                                            new object[] {new TaxKeyExtractor()});

            // Clear the internal table
            (decisionTable as IEncapsulatedDecisionTableIoc<Type, ComputeTax>).Clear();

            // load the table by (in this model) matching prefixes of Types 
            Array.ForEach(computerTypes, c =>
                {
                    string prefix = c.Name.Substring(0, c.Name.IndexOf("Compute"));
                    Type authority = authorityTypes.FirstOrDefault(s => s.Name.StartsWith(prefix));
                    // Create the DecisionEntry
                    object computeInstance = Activator.CreateInstance(c, false);
                    // Load it to the DecisionTable
                    (decisionTable as IEncapsulatedDecisionTableIoc<Type, ComputeTax>).Add(authority,
                                                                                           computeInstance as
                                                                                           ComputeTax);
                });

            // Register the DecisionTable in the IOC
            Container.RegisterInstance(typeof (IEncapsulatedDecisionTable<TaxItemLink, Tuple<decimal, decimal>>),
                                       decisionTable, new ContainerControlledLifetimeManager());
        }
    }

    internal class SimpleScanner
    {
        public IEnumerable<Type> FindTypes(Predicate<Type> f)
        {
            return (from t in Assembly.GetAssembly(GetType()).GetTypes() where f(t) select t);
        }
    }
}