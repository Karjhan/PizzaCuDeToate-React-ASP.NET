import React from 'react'

const HorizLineWithText = (props: { text: string | number | boolean | React.ReactElement<any, string | React.JSXElementConstructor<any>> | React.ReactFragment | React.ReactPortal | null | undefined }) => {
  return (
      <div
          style={{ display: 'flex', flexDirection: 'row', alignItems: 'center', marginTop: "1rem" }}
      >
          <div style={{ flex: 1, height: '2px', backgroundColor: 'gray' }} />

          <div>
              <p style={{ width: '50px', textAlign: 'center', marginTop: "1rem" }}><strong>{props.text}</strong></p>
          </div>

          <div style={{ flex: 1, height: '2px', backgroundColor: 'gray' }} />
      </div>
  )
}

export default HorizLineWithText
