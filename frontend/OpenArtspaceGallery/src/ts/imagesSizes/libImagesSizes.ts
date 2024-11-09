/* Private image size DTO */
type ImageSizeDto =
{
    id: string;
    name: string;
    width: number;
    height: number;
}

/* Public image size type model */
export type ImageSize =
{
    id: string;
    name: string;
    width: number;
    height: number;
}

/* DTO to model */
export function DecodeImageSizeDto(dto: ImageSizeDto) : ImageSize
{
    return {
        id: dto.id,
        name: dto.name,
        width: dto.width,
        height: dto.height
    };
}

/* Private images sizes response */
type ImagesSizesResponse =
{
    imagesSizes: ImageSize[];
}

/* Public images sizes model */
type ImagesSizes =
{
    imagesSizes: ImageSize[];
}

/* Response to model */
export function DecodeImagesSizesResponse(response: ImagesSizesResponse) : ImagesSizes
{
    return {
        imagesSizes: response.imagesSizes
    };
}