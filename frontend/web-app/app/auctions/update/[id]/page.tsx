import { getDetailedViewData } from '@/app/Actions/AuctionActions';
import Heading from '@/app/Components/Heading';
import React from 'react'
import Auctionform from '../../Auctionform';

export default async function Update({ params }: { params: { id: string } }) {
    const data = await getDetailedViewData(params.id);

    return (
        <div className='mx-auto max-w-[75%] shadow-lg p-10 bg-white rounded-lg'>
            <Heading title='Update your auction' subtitle='Please update the details of your car' />
            <Auctionform auction={data} />
        </div>
    )
}
