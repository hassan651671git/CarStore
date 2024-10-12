import { getDetailedViewData } from '@/app/Actions/AuctionActions'
import Heading from '@/app/Components/Heading';
import React from 'react'
import CarImage from '../../CarImage';
import { auth } from '@/auth';
import DetailedSpecs from './DetailedSpecs';
import { getCurrentUser } from '@/app/Actions/AuthActions';
import EditButton from './EditButton';
import DeleteButton from './DeleteButton';

export default async function Details({ params }: { params: { id: string } }) {
    const data = await getDetailedViewData(params.id);
    const user = await getCurrentUser();


    return (
        <div>
            <div className='flex justify-between'>
                <div className='flex items-center gap-3'>
                    <Heading title={`${data.make} ${data.model}`} />
                    {user?.username === data.seller && (
                        <>
                            <EditButton id={data.id} />
                            <DeleteButton id={data.id} />
                        </>

                    )}
                </div>

                <div className='flex gap-3'>
                    <h3 className='text-2xl font-semibold'>Time remaining:</h3>
                    14:14:55
                    {/* <CountdownTimer auctionEnd={data.auctionEnd} /> */}
                </div>
            </div>

            <div className='grid grid-cols-2 gap-6 mt-3'>
                <div className='w-full bg-gray-200 relative aspect-[9/5] rounded-lg overflow-hidden'>
                    <CarImage imageUrl={data.imageUrl} />
                </div>

                {/* <BidList user={user} auction={data} /> */}
            </div>

            <div className='mt-3 grid grid-cols-1 rounded-lg'>
                <DetailedSpecs auction={data} />
            </div>

        </div>
    )


}
