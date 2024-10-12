'use client'
import { error } from 'console';
import { Button, TextInput } from 'flowbite-react';
import React, { useEffect } from 'react'
import { FieldValues, useForm } from 'react-hook-form'
import Input from '../Components/Input';
import DateInput from '../Components/DateInput';
import { fetchWrapper } from '../lib/FetchWrapper';
import { usePathname, useRouter } from 'next/navigation';
import { createAuction, updateAuction } from '../Actions/AuctionActions';
import toast from 'react-hot-toast';
import { Auction } from '@/types';

type props = {
    auction?: Auction
}
export default function Auctionform({ auction }: props) {

    const { control, handleSubmit, setFocus, reset,
        formState: { isDirty, isSubmitting, isValid, errors } } = useForm({ mode: 'onTouched' }
        );

    const router = useRouter();
    const pathName = usePathname();

    async function onsubmit(data: FieldValues) {


        try {
            let id = '';
            let res;
            if (pathName === '/auctions/create') {
                res = await createAuction(data);
                id = res.id;
            } else {
                if (auction) {
                    res = await updateAuction(data, auction.id);
                    id = auction.id;
                }
            }
            if (res.error) {
                throw res.error;
            }
            router.push(`/auctions/details/${id}`)
        }
        catch (error: any) {
            console.log(error)
            toast.error(error.status + ' ' + error.message);
        }
    }

    useEffect(() => {
        const { make, model, mileage, color, year } = auction;
        if (auction) {
            reset({ make, model, mileage, color, year })
        }
        setFocus('make')
    },
        [setFocus]
    )

    return (
        <form className='flex flex-col mt-3 ' onSubmit={handleSubmit(onsubmit)} >

            <Input
                name='make'
                label='make'

                control={control}
                rules={{ required: 'make is required' }}
            />

            <Input
                name='model'
                label='model'

                control={control}
                rules={{ required: 'model is required' }}
            />

            <Input
                name='color'
                label='color'

                control={control}
                rules={{ required: 'color is required' }}
            />

            <div className="grid grid-cols-2 gap-3">
                <Input
                    name='year'
                    label='year'
                    type='number'

                    control={control}
                    rules={{ required: 'year is required' }}
                />

                <Input
                    name='mileage'
                    label='mileage'
                    type='number'
                    control={control}
                    rules={{ required: 'mileage is required' }}
                />
            </div>
            {pathName === '/auction/create' &&
                <>
                    <Input
                        name='imageUrl'
                        label='Image URL'

                        control={control}
                        rules={{ required: 'Image URL is required' }}
                    />

                    <div className="grid grid-cols-2 gap-3">
                        <Input
                            name='reservePrice'
                            label='Reserve price (enter 0 if no reserve)'

                            type='number'
                            control={control}
                            rules={{ required: 'Reserve is required' }}
                        />

                        <DateInput
                            label='Auction end date/time'
                            name='auctionEnd'
                            control={control}
                            dateFormat='dd MMMM yyyy h:mm a'
                            showTimeSelect
                            rules={{ required: 'Auction end date is required' }} />

                    </div>
                </>
            }


            <div className="flex justify-between">
                <Button outline color='gray' >cancel</Button>
                <Button
                    isProcessing={isSubmitting}
                    // disabled={!isValid} 
                    outline
                    color='success'
                    type='submit'
                >Submit</Button>
            </div>

        </form>
    )
}
