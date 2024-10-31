import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr'
import { useEffect, useState } from 'react'
import { useParams, useNavigate } from 'react-router-dom';
import { DecodeToken, GetAccessToken } from '../services/cookies.service'
import { MessageInfo } from '../types/message.types'
import { ChatInfo } from '../types/chat.types'
import chatService from '../services/chat.service'
import { Chat } from './Chat'
import { Container } from './Container'
import { UpdateMessageDialog } from './dialogs/update-message-dialog'

export const ChatRoom = () => {
  const { id } = useParams()
  const navigate = useNavigate()
  const [userId, setUserId] = useState<string>()
  const accessToken = GetAccessToken()
  const [cnctn, setCnctn] = useState<HubConnection | null>()
  const [chat, setChat] = useState<ChatInfo>()
  const [messages, setMessages] = useState<MessageInfo[]>([])

  if (!id) {
    navigate("/users")
  }

  useEffect(() => {
    const getUserId = async (token: string) => {
      const res = await DecodeToken(token)
      setUserId(res.userId)
    }

    if (accessToken) {
      getUserId(accessToken)
    }
  }, [accessToken])

  const joinChat = async () => {
    let connection = new HubConnectionBuilder()
      .withUrl(import.meta.env.VITE_API_URL + '/chatHub')
      .withAutomaticReconnect()
      .build()

    connection.on("ReceiveMessage", (userId, username, messageId, message) => {
      setMessages((messages) => [...messages, { userId, username, messageId, message}])
    })

    connection.on("MessageDeleted", (messageId) => {
      setMessages((prevMessages) => prevMessages.filter(m => m.messageId !== messageId))
    })

    connection.on("MessageUpdated", (messageId, content) => {
      setMessages((prevMessages) =>
        prevMessages.map(
          (msg) =>
            msg.messageId === messageId ? {...msg, message: content} : msg
        )
      )
    })

    try {
      await connection.start()
      await connection.invoke("JoinChat", {
        userId: userId,
        chatId: id
      })

      setCnctn(connection)

    } catch (err: unknown) {
      console.error(err)
    }
  }

  useEffect(() => {
    const fetchChat = async () => {
      const res = await chatService.Get({ id: id! })
      setChat(res.data.data)
    }

    if (!chat) {
      fetchChat()
    }
  }, [chat])

  useEffect(() => {
    if (id && chat) {
      joinChat()
    } 
  }, [id, chat])

  const sendMessage = (message: string) => {
    cnctn?.invoke('SendMessage', message)
  }

  const deleteMessage = (messageId: string) => {
    cnctn?.invoke('DeleteMessage', messageId)
    setMessages(prev => prev.filter(m => m.messageId !== messageId))
  }

  const updateMessage = (messageId: string, message: string) => {
    cnctn?.invoke('UpdateMessage', messageId, message)
  }

  const closeChat = async () => {
    await cnctn?.stop()
    setCnctn(null)
    navigate("/users")
  }

  return <Container className='h-screen'>
    <div className='h-full flex items-center justify-center'>
    {
      cnctn && <Chat userId={userId!} chatName={chat?.name!} messages={messages!} closeChat={closeChat} sendMessage={sendMessage} deleteMessage={deleteMessage}/>
    }
    </div>
    <UpdateMessageDialog updateMessage={updateMessage} />
  </Container>
}