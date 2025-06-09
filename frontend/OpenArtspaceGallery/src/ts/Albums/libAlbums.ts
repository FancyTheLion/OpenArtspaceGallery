/* Public albums type */
export type Album =
{
    id: string;
    parent: string | null;
    name: string;
    creationTime: Date;
}

/* Private albums DTO */
type AlbumDto =
{
    id: string;
    parent: string | null;
    name: string;
    creationTime: string;
}

/* DTO to model */
export function DecodeAlbumDto(dto: AlbumDto) : Album
{
    return {
        id: dto.id,
        parent: dto.parent,
        name: dto.name,
        creationTime: new Date(dto.creationTime),
    };
}

/* Public albums hierarchy type */
export type AlbumHierarchy =
{
    id: string;
    name: string;
}

/* Private albums hierarchy DTO */
type AlbumHierarchyDto =
{
    id: string;
    name: string;
}

/* DTO to model */
export function DecodeAlbumHierarchyDto(dto: AlbumHierarchyDto) : AlbumHierarchy
{
    return {
        id: dto.id,
        name: dto.name,
    };
}