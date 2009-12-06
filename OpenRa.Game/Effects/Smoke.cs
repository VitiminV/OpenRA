﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenRa.Game.Graphics;

namespace OpenRa.Game.Effects
{
	class Smoke : IEffect
	{
		readonly int2 pos;
		readonly Animation anim = new Animation("smokey");
		bool removed;

		public Smoke(int2 pos)
		{
			this.pos = pos;
			anim.PlayThen("idle",
				() =>
				{
					removed = true;
					Game.world.AddFrameEndTask(w => w.Remove(this));
				});
		}

		public void Tick()
		{
			anim.Tick();
		}

		public IEnumerable<Tuple<Sprite, float2, int>> Render()
		{
			if (!removed)
				yield return Tuple.New(anim.Image, pos.ToFloat2() - .5f * anim.Image.size, 0);
		}
	}
}
