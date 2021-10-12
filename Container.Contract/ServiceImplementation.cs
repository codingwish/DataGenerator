using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Core.Container.Infrastructure
{
    public enum ServiceImplementation
    {
        /// <summary>
        /// Return a new instance every time the type is injected.
        /// </summary>
        Transient,

        /// <summary>
        /// Return the same instance one it was created.
        /// </summary>
        Singleton,

        /// <summary>
        /// Return a new instance for every scope (e.g. HTTP request).
        /// </summary>
        Scoped
    }
}
