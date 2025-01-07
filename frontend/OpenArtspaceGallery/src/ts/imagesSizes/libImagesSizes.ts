/* Private image size DTO */
import {DecodeExistenceDto, ExistenceDto} from "../Shared/Libs/libSharedModels.ts";

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

/* Create new image size type model */
export type NewImageSize =
{
    name: string;
    width: number;
    height: number;
}

/* Image name existence response */
type ImageSizeNameExistenceResponse =
{
    existence: ExistenceDto
}

/* Decode image size existence by name response */
export function DecodeImageSizeNameExistenceResponse(response: ImageSizeNameExistenceResponse) : boolean
{
    return DecodeExistenceDto(response.existence).isExist
}

/* Image dimensions existence response */
type ImageSizeDimensionsExistenceResponse =
    {
        dimensionsExistence: ExistenceDto
    }

/* Decode image size existence by dimensions response */
export function DecodeImageSizeDimensionsExistenceResponse(response: ImageSizeDimensionsExistenceResponse) : boolean
{
    return DecodeExistenceDto(response.dimensionsExistence).isExist
}