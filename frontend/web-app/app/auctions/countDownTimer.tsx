'use clinet'
import React from 'react'
import Countdown from 'react-countdown';
type props = {
    auctionEnd: string;

}

const renderer = ({ days, hours, minutes, seconds, completed }:
    { days: number, hours: number, minutes: number, seconds: number, completed: boolean }) => {
    if (completed) {
        // Render a completed state
        return <span>Finished</span>
    } else {
        // Render a countdown
        return <span>{days}:{hours}:{minutes}:{seconds}</span>;
    }
};

export default function countDownTimer({auctionEnd}:props) {
    return (
        <div>
            <Countdown date={auctionEnd} renderer={renderer}/>
        </div>
    )
}
