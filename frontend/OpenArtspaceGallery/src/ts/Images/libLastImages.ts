/* DTO (from backend) */
export type LastImageDto = {
    id: string;
    name: string;
    description: string;
    albumId: string;
    thumbnailId: string;
    creationTime: string;
}

/* Last image model */
export type LastImage = {
    id: string;
    name: string;
    description: string;
    albumId: string;
    thumbnailId: string;
    creationTime: Date;
}

/* DTO to model */
export function DecodeLastImageDto(dto: LastImageDto) : LastImage
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

/* Public last images response */
type LastImagesResponse =
    {
        lastImages: LastImage[];
    }

/* Private last images response */
type LastImagesResponseDto =
    {
        lastImages: LastImageDto[];
    }

/* Response to model */
export function DecodeLastImagesResponse(response: LastImagesResponseDto) : LastImagesResponse
{
    return {
        lastImages: response
            .lastImages
            .map(DecodeLastImageDto)
    };
}
