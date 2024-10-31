import { Cross2Icon, PaperPlaneIcon } from '@radix-ui/react-icons'
import { MessageInfo } from '../types/message.types'
import { H3 } from './typography/h3'
import { Message } from './ui/message'
import { useEffect, useRef, useState } from 'react'
import { Input } from './ui/input'
import { Button } from './ui/button'

type Props = {
  userId: string
  chatName: string
  messages: MessageInfo[]
  closeChat: () => void
  sendMessage: (message: string) => void
  deleteMessage: (messageId: string) => void
}

export const Chat = ({ userId, chatName, messages, closeChat, sendMessage, deleteMessage }: Props) => {
  const [message, setMessage] = useState<string>()
  const messageEndRef = useRef<HTMLSpanElement>(null)

  useEffect(() => {
    messageEndRef?.current?.scrollIntoView()
  }, [messages])

  const onSendMessage = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    if (!message) return

    sendMessage(message)
    setMessage('')
  }
  
  return <div className='flex flex-col gap-4 w-full h-full p-8 rounded-2xl border border-accent shadow-accent shadow-lg'>
    <div className='flex justify-between mb-5'>
      <H3 text={chatName} />
      <Cross2Icon className='h-6 w-6 text-accent-foreground' onClick={closeChat} />
    </div>

    <div className='p-4 flex flex-col overflow-auto scroll-smooth h-full gap-3 pb-3'>
      {
        messages?.map((msg, idx) => <Message messageId={msg.messageId} deleteMessage={deleteMessage} editable={msg.userId === userId} key={`msg-${idx}`} message={msg.message} username={msg.username} />)
      }
      <span ref={messageEndRef} />
    </div>

    <form onSubmit={(e) => onSendMessage(e)} className='w-full flex items-center gap-2'>
      <Input className='w-full' placeholder='Введите сообщение...' value={message} onChange={(e) => setMessage(e.target.value)} />
      <Button type='submit'><PaperPlaneIcon className='h-5 w-5 text-background' /></Button>
    </form>
  </div>
}