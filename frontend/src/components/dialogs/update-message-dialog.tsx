import { useUpdateMessageStore } from '../../stores/updateMessageStore'
import { UpdateMessageForm } from '../forms/update-message-form'
import { Dialog, DialogContent, DialogHeader, DialogTitle } from '../ui/dialog'

type Props = {
  updateMessage: (messageId: string, message: string) => void
}

export const UpdateMessageDialog = ({ updateMessage }: Props) => {
  const { messageId, messageContent, isOpen, close } = useUpdateMessageStore()
  return <Dialog open={isOpen} onOpenChange={close}>
    <DialogContent className='sm:max-w-[460px]'>
      <DialogHeader>
        <DialogTitle>Обновление сообщения</DialogTitle>
      </DialogHeader>
      <UpdateMessageForm id={messageId!} currContent={messageContent!} updateMessage={updateMessage} />
    </DialogContent>
  </Dialog>
}