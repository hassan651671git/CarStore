import React from 'react'
import { AiOutlineCar } from 'react-icons/ai'
import Search from './Search'
import Logo from './logo'
import LoginButton from './LoginButton'
import { getCurrentUser } from '../Actions/AuthActions'
import UserActions from './UserActions'

export default async function navbar() {
  const user = await getCurrentUser();
  return (
    <header className='
    sticky top-0 z-50 flex justify-between bg-white p-5 text-center text-gray-8000 shadow-md
    '>
      <Logo />
      <Search />
      {user ?
        (<UserActions user={user} />) :
        (<LoginButton />)
      }

    </header>
  )
}
