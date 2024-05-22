using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Models
{
    public class BaseEntity
    {

        public int Id { get; protected set; }

        public BaseEntity()
        {
            Id = GenerateUserID();
        }
        private static int GenerateUserID()
        {
            Guid newGuid = Guid.NewGuid();
            byte[] newGuidByte = newGuid.ToByteArray();
            int intId = BitConverter.ToInt32(newGuidByte, 0);

            return intId;
        }
    }
}
