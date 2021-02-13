using System;
using cAmp.Libraries.Common.Interfaces;

namespace cAmp.Libraries.Common.Records
{
    public record PlayList : AbstractcAmpRecord
    {
        public PlayList()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public Guid OwnerUserId { get; init; }

        public bool IsShared { get; init; }

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
