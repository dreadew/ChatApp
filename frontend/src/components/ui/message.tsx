import { Cross2Icon, Pencil2Icon } from '@radix-ui/react-icons'
import { Button } from './button'
import { useUpdateMessageStore } from '../../stores/updateMessageStore'

type Props = {
  username: string
  message: string
  messageId: string
  editable: boolean
  deleteMessage: (messageId: string) => void
}

export const Message = ({ username, message, messageId, deleteMessage, editable = false }: Props) => {
  const { open } = useUpdateMessageStore()

  return <div className='w-full'>
    <span className='text-sm text-accent-foreground'>{username}</span>
    <div className='w-full flex justify-between gap-2'>
      <p className='p-2 flex justify-between items-start bg-accent/50 text-foreground font-medium rounded-lg shadow-lg'>
        {message}
      </p>
      {
        editable && <span className='flex items-center gap-2'>
          <Button onClick={() => deleteMessage(messageId)} variant={'ghost'}>
            <Cross2Icon className='h-5 w-5 text-foreground' />
          </Button>
          <Button onClick={() => open(messageId, message)} variant={'ghost'}>
            <Pencil2Icon className='h-5 w-5 text-foreground' />
          </Button>
        </span>
      }
  </div>
</div>
}