﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Jtc.CsQuery
{
    public partial class CsQuery
    {
        #region implementation-specific methods
        /// <summary>
        /// Represents the full, parsed DOM for an object created with an HTML parameter
        /// </summary>
        public IDomRoot Document
        {
            get
            {
                if (_Document == null)
                {
                    _Document = new DomRoot();   
                }
                return _Document;
            }
            protected set
            {
                _Document = value;
            }
        } 
        protected IDomRoot _Document = null;

        /// <summary>
        ///  The selector (parsed) used to create this instance
        /// </summary>
        public CsQuerySelectors Selectors
        {
            get
            {
                return _Selectors;
            }
            protected set
            {
                _Selectors = value;
            }
        }
        protected CsQuerySelectors _Selectors = null;
        /// <summary>
        /// Renders just the selection set completely.
        /// </summary>
        /// <returns></returns>
        public string RenderSelection()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IDomObject elm in this)
            {
                sb.Append(elm.Render());
            }
            return sb.ToString();
        }

        /// <summary>
        /// Returns the HTML for all selected documents, separated by commas. No inner html or children are included.
        /// </summary>
        /// 
        public string SelectionHtml()
        {
            return SelectionHtml(false);
        }

        public string SelectionHtml(bool includeInner)
        {
            StringBuilder sb = new StringBuilder();
            foreach (IDomObject elm in this)
            {

                sb.Append(sb.Length == 0 ? String.Empty : ", ");
                sb.Append(includeInner ? elm.Render() : elm.ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// Renders the DOM to a string
        /// </summary>
        /// <returns></returns>
        public string Render()
        {
            return Document.Render();
        }
        public string Render(DomRenderingOptions renderingOptions)
        {
            Document.DomRenderingOptions = renderingOptions;
            return Render();
        }
        /// <summary>
        /// Returns a new empty CsQuery object bound to this domain
        /// </summary>
        /// <returns></returns>
        public CsQuery New()
        {
            CsQuery csq = new CsQuery();
            csq.CsQueryParent = this;
            return csq;
        }

        #endregion
        #region nonstandard (extension) methods
        /// <summary>
        /// Removes one of two selectors/objects based on the value of the first parameter. The remaining one is
        /// explicitly shown. True keeps the first, false keeps the 2nd.
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public CsQuery KeepOne(bool which, string trueContent, string falseContent)
        {
            return KeepOne(which ? 0 : 1, trueContent, falseContent);
        }
        public CsQuery KeepOne(bool which, CsQuery trueContent, CsQuery falseContent)
        {
            return KeepOne(which ? 0 : 1, trueContent, falseContent);
        }
        /// <summary>
        /// Removes one of two selectors/objects based on the value of the first parameter. The remaining one is
        /// explicitly shown. True keeps the first, false keeps the 2nd. Which is zero-based.
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public CsQuery KeepOne(int which, params string[] content)
        {
            CsQuery[] arr = new CsQuery[content.Length];
            for (int i = 0; i < content.Length; i++)
            {
                arr[i] = Select(content[i]);
            }
            return KeepOne(which, arr);
        }
        public CsQuery KeepOne(int which, params CsQuery[] content)
        {
            for (int i = 0; i < content.Length; i++)
            {
                if (i == which)
                {
                    content[i].Show();
                }
                else
                {
                    content[i].Remove();
                }
            }
            return this;
        }
        /// <summary>
        /// Conditionally includes a selection. This is the equivalent of calling Remove() only when "include" is false
        /// (extension of jQuery API)
        /// </summary>
        /// <returns></returns>
        public CsQuery IncludeWhen(bool include)
        {
            if (!include)
            {
                Remove();
            }
            return this;
        }
        /// <summary>
        /// Returns an attribute value as a nullable integer, or null if not an integer
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int? AttrInt(string name)
        {
            string value;
            if (Length > 0 && this[0].TryGetAttribute(name, out value))
            {
                int intValue;
                if (int.TryParse(value, out intValue))
                {
                    return intValue;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
        #endregion

        public override string ToString()
        {
            return SelectionHtml();
        }

        #region IEnumerable<IDomElement> Members

        public IEnumerator<IDomObject> GetEnumerator()
        {
            return Selection.GetEnumerator();
        }
        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Selection.GetEnumerator();
        }

        #endregion

    }
}