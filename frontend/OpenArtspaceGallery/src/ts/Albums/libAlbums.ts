/* Public album type */
export type Album =
{
    id: string;
    parent: string | null;
    name: string;
    creationTime: Date;
}

/* Private album DTO */
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

