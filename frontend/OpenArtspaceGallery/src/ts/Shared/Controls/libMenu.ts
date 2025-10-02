/* Shared menu. Minimum 2 elements  */
export type MenuItemsBase =
    {
        id: string
        name: string
    }

/* To add additional fields */
export type MenuItemsExtended = MenuItemsBase & {
    width: number
    height: number
}