﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsQuery.StringScanner.Implementation;

namespace CsQuery.StringScanner.Patterns
{
    
    public class HTMLTagSelectorName: EscapedString
    {
        public HTMLTagSelectorName() : 
            base(IsValidTagName)
        {

        }
        /// <summary>
        /// Match a pattern for am attribute name selector
        /// </summary>
        /// <param name="index"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        protected static bool IsValidTagName(int index, char character)
        {
            if (index == 0)
            {
                return CharacterData.IsType(character, CharacterType.HtmlTagSelectorStart);
            }
            else
            {
                return CharacterData.IsType(character, CharacterType.HtmlTagSelectorExceptStart);
            }
        }
    }
}
