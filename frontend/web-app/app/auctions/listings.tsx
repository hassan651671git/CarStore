'use client'
import React, { useEffect, useState } from 'react'
import { json } from 'stream/consumers';
import AuctionCard from './AuctionCard';
import { Auction, PagedResult } from '@/types';
import AppPagination from '../Components/AppPagination';
import { getData } from '../Actions/AuctionActions';
import Filters from './filters';
import { useParamsStore } from '@/hooks/useParamsStore';
import { useShallow } from 'zustand/react/shallow';
import qs from 'query-string';
import EmptyFilter from '../Components/EmptyFilter';


export default function listings() {

  const [data, setData] = useState<PagedResult<Auction>>();
  const params = useParamsStore(useShallow(state => ({
    pageNumber: state.pageNumber,
    pageSize: state.pageSize,
    searchTerm: state.searchTerm,
    orderBy: state.orderBy,
    filterBy: state.filterBy,
    seller: state.seller,
    winner: state.winner
  })))

  const setParams = useParamsStore(state => state.setParams);
  const url = qs.stringifyUrl({ url: '', query: params })

  function setPageNumber(pageNumber: number) {
    setParams({ pageNumber: pageNumber })
  }


  const [auctions, setAuctions] = useState<Auction[]>([]);
  const [pageCount, setPageCount] = useState<number>(0);
  const [pagesize, setPageSize] = useState<number>(4);
  useEffect(() => {
    getData(url).then((data) => {
      setData(data);
    });
  }, [url]);

  if (!data) return <h1>Loading...</h1>



  return (
    <>
      <Filters />
      {data.results.length === 0 && (
        <EmptyFilter showReset />
      )}

      {data.results.length !== 0 && <>(

        <div className='grid grid-cols-4 gap-6'>
          {data.results.map(auction => (
            <AuctionCard auction={auction} key={auction.id} />
          ))}
        </div>
        <div className='flex justify-center mt-4'>
          <AppPagination pageChanged={setPageNumber}
            currentPage={params.pageNumber} pageCount={data.pageCount} />
        </div>
        )</>}

    </>
  )
}
