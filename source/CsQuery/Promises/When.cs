﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsQuery.Promises;

namespace CsQuery
{
    /// <summary>
    /// A static provider for methods that produce Promise-related objects
    /// </summary>

    public static class When
    {
        /// <summary>
        /// Returns a new Deferred object, an object containing a promise and resolver methods.
        /// </summary>
        ///
        /// <returns>
        /// A new Deferred object.
        /// </returns>

        public static Deferred Deferred()
        {
            return new Deferred();
        }

        /// <summary>
        /// Returns a new Deferred object, an object containing a promise and resolver methods.
        /// </summary>
        ///
        /// <typeparam name="T">
        /// Generic type parameter determining the type of parameter that will be passed to the resolvers
        /// </typeparam>
        ///
        /// <returns>
        /// A new Deferred object
        /// </returns>

        public static Deferred<T> Deferred<T>()
        {
            return new Deferred<T>();
        }

        /// <summary>
        /// Returns a new promise that resolves when all of the promises passed by parameter have resolved
        /// </summary>
        ///
        /// <param name="promises">
        /// One or more IPromise objects
        /// </param>
        ///
        /// <returns>
        /// A new IPromise object
        /// </returns>

        public static IPromise All(params IPromise[] promises)
        {
            return new WhenAll(promises);

        }

        /// <summary>
        /// Returns a new promise that resolves when all of the promises passed by parameter have
        /// resolved, or when the time has elapsed
        /// </summary>
        ///
        /// <param name="timeoutMilliseconds">
        /// The timeout in milliseconds.
        /// </param>
        /// <param name="promises">
        /// One or more IPromise objects.
        /// </param>
        ///
        /// <returns>
        /// A new IPromise object.
        /// </returns>

        public static IPromise All(int timeoutMilliseconds, params IPromise[] promises)
        {
            return new WhenAll(timeoutMilliseconds,promises);
            
        }
    }
}
