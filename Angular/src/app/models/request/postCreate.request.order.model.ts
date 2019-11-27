export interface PostCreateRequestOrderModel{
    typeOfPaymentCard?: string;
    description?: string;
    currency?: string;
    userId?: string;
    orderPrintingEdition?: OrderPrintingEdition [];
}

export interface OrderPrintingEdition{
    id?: string;
    count?: number;
}