import { useState } from 'react'
import { Button } from '../ui/button'
import { Input } from '../ui/input'
import { useUpdateMessageStore } from '../../stores/updateMessageStore'

type Props = {
  id: string
  currContent: string
  updateMessage: (messageId: string, message: string) => void
}

export const UpdateMessageForm = ({ id, currContent, updateMessage }: Props) => {
  const [content, setContent] = useState<string>('')
  const { close } = useUpdateMessageStore()
  return <form onSubmit={(e) => {
    e.preventDefault()
    updateMessage(id, content)
    close()
  }} className='w-full flex flex-col gap-2'>
    <Input value={content} onChange={e => setContent(e.target.value)} className='w-full' placeholder={currContent} />
    <Button type='submit'>Обновить</Button>
  </form>
}