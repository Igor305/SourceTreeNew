import { BaseResponseModel } from './base.response.model';

export interface ResponseOrderModel extends BaseResponseModel{
    orderModel?: OrderModel []
}

export interface OrderModel {
    id?: string;
    description?: string;
    userId?: string;
    paymentId?: string;
    orderItems?: OrderItem [];
    createDateTime?: Date;
    updateDateTime?: Date;
    isDeleted?: boolean;
}

export interface OrderItem{
    amount?: number;
    currency?: string;
    count?: number;
    unitPrice?: number;
    printingEditionId?: string;
    orderId?: string; 
}