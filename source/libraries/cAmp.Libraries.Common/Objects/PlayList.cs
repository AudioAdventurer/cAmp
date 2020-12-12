using System;
using cAmp.Libraries.Common.Interfaces;

namespace cAmp.Libraries.Common.Objects
{
    public class PlayList : AbstractcAmpObject
    {
        public PlayList()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid OwnerUserId { get; set; }

        public bool IsShared { get; set; }

        public override IcAmpObject ToUserInterfaceObject()
        {
            UserInterfaceObjects.PlayList uiPlayList = new UserInterfaceObjects.PlayList
            {
                Id = Id, 
                Name = Name, 
                Description = Description
            };

            return uiPlayList;
        }
    }
}
