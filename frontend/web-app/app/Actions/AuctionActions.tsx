"use server"
import { auth } from '@/auth';
import { Auction, PagedResult } from '@/types';
import React from 'react'
import { fetchWrapper } from '../lib/FetchWrapper';
import { FieldValues } from 'react-hook-form';
import { promises } from 'dns';
import { revalidatePath } from 'next/cache';

export async function getData(query: string): Promise<PagedResult<Auction>> {
    return await fetchWrapper.get(`search${query}`);
}


export async function updateAuctionTest() {

    const session = await auth();
    const data = {
        mileage: Math.floor(Math.random() * 100000) + 1
    }

    return await fetchWrapper.put('auctions/afbee524-5972-4075-8800-7d1f9d7b0a0c', data);
}

export async function createAuction(data: FieldValues) {
    return await fetchWrapper.post('auctions', data);
}

export async function getDetailedViewData(id: any): Promise<Auction> {
    return await fetchWrapper.get(`auctions/${id}`);
}

export async function updateAuction(auction: FieldValues, id: string) {
    const res = await fetchWrapper.put(`auctions/${id}`, auction);

    revalidatePath(`/auctions/${id}`);
    return res;
}

export async function deleteAuction(id: string) {
    return fetchWrapper.del(`auctions/${id}`);
}
