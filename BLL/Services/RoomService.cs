using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RoomService : GenericService<Room>, IRoomService
    {
        public RoomService(IRoomRepository repo) : base(repo) { }
    }
}
