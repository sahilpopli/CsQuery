﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using Description = NUnit.Framework.DescriptionAttribute;
using TestContext = Microsoft.VisualStudio.TestTools.UnitTesting.TestContext;
using CsQuery;
using CsQuery.HtmlParser;
using CsQuery.Utility;

namespace CsQuery.Tests.Csharp.HtmlParser
{
    
    /// <summary>
    /// A set of tests using the big HTML5 spec document to push things a bit. For the most part these tests compare output
    /// against data obtianed from the same jQuery selector run in chrome.
    /// </summary>
    [TestFixture, TestClass]
    public class HtmlBigDomTest : CsQueryTest
    {
        


        /// <summary>
        /// This method ensures that the huge DOM gets parsed correctly by checking a few key selectors. The biggest factors that
        /// can cause problems are self-closing tags. See: TagHasImplicitClose function.
        /// 
        ///  Note that changing in that function
        ///  
        ///  case DomData.tagLI:
        ///     return newTagID == DomData.tagLI || newTagId == DomData.tagUL || newTagId == DomData.tagOL;
        ///     
        /// (e.g. checking for any other list type opener, instead of just the same one that's currently open, as it is now) causes
        /// the HTML5 spec document to parse differently than Chrome and these tests to fail. I am not sure why since I don't think that
        /// OL is a valid child of UL > LI or UL is a valid child of OL > LI. But apparently they do that in the document somewhere,
        /// e.g. open a differently-typed list as a child of an LI.
        /// 
        /// </summary>
        [Test, TestMethod]
        public void DomParsingTestWithNthChild()
        {

            // these values have been verified in Chrome with jQuery 1.7.2

            Assert.AreEqual(2704, Dom["div span:first-child"].Length);
            Assert.AreEqual(2517, Dom["div span:only-child"].Length);
            Assert.AreEqual(2, Dom["[type]"].Length);
            Assert.AreEqual(505, Dom["div:nth-child(2n+1)"].Length);
            Assert.AreEqual(13, Dom["div:nth-child(3)"].Length);
            Assert.AreEqual(534, Dom["div:nth-last-child(2n+1)"].Length);
            Assert.AreEqual(7, Dom["div:nth-last-child(3)"].Length);
            Assert.AreEqual(2605, Dom["div span:last-child"].Length);
        
        }

        [Test, TestMethod]
        public void AutoGeneratedTags()
        {

            // these values have been verified in Chrome with jQuery 1.7.2

            Assert.AreEqual(110, Dom["tbody"].Length);

        }



        public override void FixtureSetUp()
        {
            base.FixtureSetUp();
            Dom = TestDom("HTML Standard");
        }

    }

    

}