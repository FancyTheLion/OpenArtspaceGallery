/* File DTO */
export type ImageFileDto = {
    id: string;
    sizeId: string;
};

/* Image DTO */
export type ImageDto = {
    id: string;
    name: string;
    description: string;
    albumId: string;
    creationTime: string;
    files: ImageFileDto[];
};

/* File model */
export type ImageFile = {
    id: string;
    sizeId: string;
};

/* Image model */
export type ImageModel = {
    id: string;
    name: string;
    description: string;
    albumId: string;
    creationTime: Date;
    files: ImageFile[];
};

/* Private response DTO */
export type ImageResponseDto = {
    image: ImageDto;
};

/* Public response model */
export type ImageResponse = {
    image: ImageModel;
};

/* Decoder */
export function DecodeImageResponse(response: ImageResponseDto): ImageResponse {
    return {
        image: DecodeImageDto(response.image)
    };
}

/* File decoder */
export function DecodeImageFileDto(dto: ImageFileDto): ImageFile {
    return {
        id: dto.id,
        sizeId: dto.sizeId
    };
}

/* Image decoder */
export function DecodeImageDto(dto: ImageDto): ImageModel {
    return {
        id: dto.id,
        name: dto.name,
        description: dto.description,
        albumId: dto.albumId,
        creationTime: new Date(dto.creationTime),
        files: dto.files.map(DecodeImageFileDto)
    };
}