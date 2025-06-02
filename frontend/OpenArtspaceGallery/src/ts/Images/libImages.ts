export type ImageDto = {
    id: string;
    name: string;
    description: string;
    albumId: string;
    thumbnailId: string;
    creationTime: string;
}

export type Image = {
    id: string;
    name: string;
    description: string;
    albumId: string;
    thumbnailId: string;
    creationTime: Date;
}

/* DTO to model */
export function DecodeImageDto(dto: ImageDto) : Image
{
    return {
        id: dto.id,
        name: dto.name,
        description: dto.description,
        albumId: dto.albumId,
        thumbnailId: dto.thumbnailId,
        creationTime: new Date(dto.creationTime)
    }
}

/* Private images response */
type ImagesResponse =
{
    images: ImageDto[];
}

/* Response to model */
export function DecodeImagesResponse(response: ImagesResponse) : { images: ImageDto[] }
{
    return {
        images: response.images
    };
}