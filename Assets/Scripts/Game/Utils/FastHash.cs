/**
* @classdesc FastHash
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

namespace Moonjoy.Core.Utils
{
    public class FastHash
    {
        public static uint CalculateHash(string read)
        {
            var hashValue = 2654435761;
            foreach (var t in read)
            {
                hashValue += t;
                hashValue *= 2654435761;
            }

            return hashValue;
        }

        public static uint CalculateHash(uint[] read)
        {
            var hashedValue = 2654435761;
            foreach (var t in read)
            {
                hashedValue += t;
                hashedValue *= 2654435761;
            }

            return hashedValue;
        }
    }
}