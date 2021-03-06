﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestContext = Microsoft.VisualStudio.TestTools.UnitTesting.TestContext;
using CsQuery.Utility;
using System.Reflection;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;
using CsQuery.Mvc;

namespace CsQuery.Mvc.Tests
{
    [SetUpFixture, TestClass]
    public class TestConfig
    {
        public static MvcAppHost Host;

#if DEBUG 
        const string build = "debug";
#else
        const string build="release";
#endif
        
        [SetUp]
        public static void AssemblySetup()
        {
            string appPath = Path.GetDirectoryName(new System.Diagnostics.StackFrame(true).GetFileName());

            Host = MvcAppHost.CreateApplicationHost<MvcTestApp>(appPath, appPath + "\\bin\\"+build);
        }

        [TearDown]
        public static void AssemblyTeardown()
        {
            Host.Dispose();
        }

        /// <summary>
        /// Set up this test run - configuration of the file name is done in the static constructor so
        /// it's not starting a new file for each test fixture.
        /// </summary>

        [AssemblyInitialize]
        public static void SetupTestRun(TestContext context)
        {
            AssemblySetup();

        }

        [AssemblyCleanup]
        public static void CleanupTestRun()
        {
            AssemblyTeardown();
        }

    }
}
