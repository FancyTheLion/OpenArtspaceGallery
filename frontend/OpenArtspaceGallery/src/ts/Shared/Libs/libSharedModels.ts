/* Shared DTO for existence responses */
export type ExistenceDto =
{
    isExist: boolean;
}

/* Shared existence model */
export type Existence =
{
    isExist: boolean;
}

/* DTO to model */
export function DecodeExistenceDto(dto: ExistenceDto) : Existence
{
    return {
        isExist: dto.isExist
    };
}