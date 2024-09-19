"use server"
import { Auction, PagedResult } from '@/types';
import React from 'react'

export async function getData(query: string): Promise<PagedResult<Auction>> {

    const res = await fetch(`http://localhost:7002/api/search${query}`);

    if (!res.ok) {
        throw new Error('Failed to fetch data!');
    }

    return res.json();
}
