using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db
{
    public static class Rozsirenia
    {
        public static void Add<T>(this LinkedList<T> paLinkedList, T paElement)
        {
            if (paLinkedList.Last == null)
                paLinkedList.AddFirst(paElement);
            else
                paLinkedList.AddAfter(paLinkedList.Last, paElement);
        }
    }
}
