const MessageContainer = ({messages}) => {
    return <div>
        {
            messages.map((msg, index) => 
                <table key={index}>
                    <tr>
                        <td>{msg.msg} - {msg.username}</td>    
                    </tr>   
                </table>
            )
        }
    </div>
}

export default MessageContainer; 