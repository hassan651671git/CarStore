import React from 'react'
import { AiOutlineCar } from 'react-icons/ai'
import Search from './Search'
import Logo from './logo'

export default function navbar() {
  return (
    <header className='
    sticky top-0 z-50 flex justify-between bg-white p-5 text-center text-gray-8000 shadow-md
    '>
      <Logo />
      <Search />
      <div>login</div>
    </header>
  )
}
