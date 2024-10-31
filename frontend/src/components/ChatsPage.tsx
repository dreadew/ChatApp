import { useContext, useEffect, useState } from 'react'
import { UserInfo } from '../types/user.types'
import { AuthContext } from '../contexts/auth-context'
import userService from '../services/user.service'
import { DecodeToken } from '../services/cookies.service'
import { Menu } from './Menu'
import { Container } from './Container'
import { H2 } from './typography/h2'
import { Link, useNavigate } from "react-router-dom"
import { Cross2Icon, EnvelopeClosedIcon } from '@radix-ui/react-icons'
import chatService from '../services/chat.service'
import { Button } from './ui/button'

export const ChatsPage = () => {
  const [user, setUser] = useState<UserInfo>()
  const { token, isAuthorized } = useContext(AuthContext)
  const navigate = useNavigate()

  useEffect(() => {
    if (!isAuthorized) {
      navigate("/")
    }
  }, [isAuthorized, navigate])

  useEffect(() => {
    const fetchUser = async () => {
      const decodedToken = await DecodeToken(token!)
      const res = await userService.Get({ id: decodedToken.userId })
      setUser(res.data.data)
    }

    if (user == null) {
      fetchUser()
    }
  }, [])
  
  const deleteChat = async (id: string, creatorId: string) => {
    await chatService.Delete({ id, creatorId })
    window.location.reload()
  }

  return <>
    <Container>
      <div className='h-screen flex flex-col gap-6'>
        <H2 text={"Список чатов"} />
        <ul className='mt-6 w-full flex flex-col gap-3'>
          {
            user?.chats.map((chat, idx) => (
              <li className='flex items-center gap-2 justify-between' key={`chat-${idx}`}>
                <div className='w-full'>
                  <Link to={`/chat/${chat.id}`}>
                    <li className='w-full p-2 flex items-center justify-between font-medium text-foreground transition-all betterhover:hover:opacity-80 betterhover:hover:bg-accent/50 rounded-lg'>
                      <div className='flex items-center gap-6'>
                        <EnvelopeClosedIcon className='h-6 w-6 text-accent-foreground' />
                        {chat.name}
                      </div>
                      <div className='flex items-center gap-2'>
                        <span className='text-xs text-accent-foreground'>
                          {chat.createdAt.split('T')[0]}
                        </span>
                      </div>
                    </li>
                  </Link>
                </div>
                <Button onClick={() => deleteChat(chat.id, user.id)} variant={'outline'}>
                  <Cross2Icon className='h-5 w-5 text-foreground' />
                </Button>
              </li>
              )
            )
          }
          {
            user?.chats.length == 0 &&
            <span className='text-md font-medium text-foreground'>У вас еще нет чатов</span>
          }
        </ul>
      </div>
    </Container>
    <Menu />
  </>
}