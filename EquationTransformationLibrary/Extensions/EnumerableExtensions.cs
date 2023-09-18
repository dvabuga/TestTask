using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EquationTransformationLibrary.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<Task<string>>> ToTasksBatch<T>(
            this IEnumerable<T> enumerable,
            int batchSize)
        {
            if (batchSize < 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            var counter = 0;
            var batch = new List<Task<string>>();
            foreach (var item in enumerable)
            {
                var task = new Task<string>(() => Core.Core.ConvertEquation(item.ToString()));
                task.Start();
                batch.Add(task);
                if (++counter % batchSize == 0)
                {
                    yield return batch;
                    batch = new List<Task<string>>();
                }
            }
            if (batch.Count != 0)
            {
                yield return batch;
            }
        }
    }
}
