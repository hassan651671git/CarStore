import React from 'react'
import Auctionform from '../Auctionform'
import Heading from '@/app/Components/Heading'

export default function Create() {
    return (
        <div className='mx-auto mx-w-[75%] shadow-lg p-10 bg-white rounded-lg'>
            <Heading title='Sell your car!' subtitle='Please enter the details for your car' />
            <Auctionform />
        </div>
    )
}
