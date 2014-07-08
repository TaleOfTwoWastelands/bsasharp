﻿using BSAsharp.Extensions;
using BSAsharp.Format;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BSAsharp
{
    /// <summary>
    /// A managed representation of a BSA folder.
    /// </summary>
    [DebuggerDisplay("{Path} ({Count})")]
    public class BSAFolder : SortedSet<BSAFile>, IHashed
    {
        public string Path { get; private set; }

        public ulong Hash { get; private set; }

        public BSAFolder(string path, IEnumerable<BSAFile> children = null)
            : this(children)
        {
            //Must be all lower case, and use backslash as directory delimiter
            this.Path = BSAFile.FixPath(path);
            this.Hash = Util.CreateHash(Path, "");
        }
        private BSAFolder(IEnumerable<BSAFile> collection)
            : base(collection ?? new SortedSet<BSAFile>(), HashComparer.Instance)
        {
        }

        public override string ToString()
        {
            return Path;
        }

        public bool IsParent(BSAFile file)
        {
            return Contains(file) && System.IO.Path.Combine(Path, file.Name).Equals(file.Filename);
        }
    }
}