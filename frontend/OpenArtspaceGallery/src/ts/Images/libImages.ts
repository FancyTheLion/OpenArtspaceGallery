/* DTO (from backend) */
export type ImageDto = {
    id: string;
    name: string;
    description: string;
    albumId: string;
    thumbnailId: string;
    creationTime: string;
}

/* Image model */
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

/* Public images response */
type ImagesResponse =
{
    images: Image[];
}

/* Private images response */
type ImagesResponseDto =
{
    images: ImageDto[];
}

/* Response to model */
export function DecodeImagesResponse(response: ImagesResponseDto) : ImagesResponse
{
    return {
        images: response
            .images
            .map(DecodeImageDto)
    };
}