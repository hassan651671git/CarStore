'use client'

import { useParamsStore } from '@/hooks/useParamsStore'
import Link from 'next/link'
import { usePathname, useRouter } from 'next/navigation'
import React from 'react'
import { AiOutlineCar } from 'react-icons/ai'

export default function Logo() {
    // const router = useRouter();
    // const pathname = usePathname();
    const reset = useParamsStore(state => state.reset);

    function doReset() {
        // if (pathname !== '/') router.push('/');
        reset();
    }

    return (
        <div onClick={doReset} className='cursor-pointer flex items-center gap-2 text-3xl font-semibold text-red-500'>
            <AiOutlineCar size={34} />
            <Link href='/'>Carsties Auctions</Link>
        </div>
    )
}
